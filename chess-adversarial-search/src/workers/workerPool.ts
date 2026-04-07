export type WorkerResult = {
  bestMove: string;
  bestValue: number;
  nodes: number;
  timeMs: number;
};

type WorkerJob = {
  fen: string;
  depth: number;
  moves: string[];
  resolve: (result: WorkerResult) => void;
};

export class WorkerPool {
  public workers: Worker[] = [];
  public queue: WorkerJob[] = [];
  private idleWorkers: Worker[] = [];

  constructor(workerCount: number, workerUrl: string) {
    for (let i = 0; i < workerCount; i++) {
      const worker = new Worker(workerUrl, { type: "module" });
      worker.onmessage = (e) => this.handleMessage(worker, e);
      this.workers.push(worker);
      this.idleWorkers.push(worker);
    }
  }

  private handleMessage(worker: Worker, event: MessageEvent) {
    const result = event.data as WorkerResult;
    const job = this.currentJobs.get(worker);
    if (job) {
      job.resolve(result);
      this.currentJobs.delete(worker);
    }
    this.idleWorkers.push(worker);
    this.runNext();
  }

  private currentJobs = new Map<Worker, WorkerJob>();

  private runNext() {
    if (this.queue.length === 0 || this.idleWorkers.length === 0) return;

    const worker = this.idleWorkers.shift()!;
    const job = this.queue.shift()!;
    this.currentJobs.set(worker, job);

    worker.postMessage({
      fen: job.fen,
      depth: job.depth,
      moves: job.moves,
    });
  }

  runJob(job: Omit<WorkerJob, "resolve">): Promise<WorkerResult> {
    return new Promise((resolve) => {
      this.queue.push({ ...job, resolve });
      this.runNext();
    });
  }

  terminate() {
    this.workers.forEach((w) => w.terminate());
  }
}
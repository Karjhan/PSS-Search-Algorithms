export default function ControlsPanel({
  running,
  setRunning,
  algorithm,
  setAlgorithm,
  onReset,
  onRandom,
  speed,
  setSpeed,
  temperature,
  setTemperature,
  coolingRate,
  setCoolingRate
}: any) {
  return (
    <>
      <div className="controls">
        <button onClick={() => setRunning(!running)}>
          {running ? "Stop" : "Start"}
        </button>

        <button onClick={onReset} disabled={running}>Clear</button>
        <button onClick={onRandom} disabled={running}>Random</button>

        <select
          value={algorithm}
          onChange={e => setAlgorithm(e.target.value)}
          disabled={running}
        >
          <option value="manual">Manual</option>
          <option value="random">Random</option>
          <option value="annealing">Annealing</option>
        </select>

        <input
          type="range"
          min="10"
          max="500"
          value={speed}
          onChange={e => setSpeed(Number(e.target.value))}
        />
      </div>
      {algorithm === "annealing" && (
          <div className="annealing-panel">
            <input
              type="number"
              step="0.1"
              value={temperature}
              onChange={e => setTemperature(Number(e.target.value))}
              disabled={running}
              placeholder="Temp"
            />

            <input
              type="number"
              step="0.001"
              value={coolingRate}
              onChange={e => setCoolingRate(Number(e.target.value))}
              disabled={running}
              placeholder="Cooling"
            />
          </div>
        )}
    </>
  );
}
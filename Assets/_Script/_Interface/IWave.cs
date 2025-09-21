using System;

public interface IWave
{
    event Action<IWave> OnWaveFinished;
    void ResetWave();
}

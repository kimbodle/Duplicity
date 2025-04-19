public interface IMission
{
    bool IsMissionCompleted { get; }
    void Initialize();
    bool CheckCompletion();
}

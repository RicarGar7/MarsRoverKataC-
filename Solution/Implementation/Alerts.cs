namespace Test;

public class Alerts: List<Alert>
{
    public bool HasOperationBeenCancelled()
    {
        return this.Any();
    }
}
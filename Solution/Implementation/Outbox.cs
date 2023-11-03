namespace Test;

public interface Outbox
{
    public void publish(Message message);
}
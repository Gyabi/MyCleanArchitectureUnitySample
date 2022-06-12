public class SendPacket
{
    public Player player;
    public SendBlockActions blockAction;
    public SendPacket(Player player, SendBlockActions action)
    {
        this.player = player;
        this.blockAction = action;
    }
}
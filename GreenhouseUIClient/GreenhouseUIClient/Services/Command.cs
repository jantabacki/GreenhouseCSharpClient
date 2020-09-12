namespace GreenhouseInterface
{
    public class Command
    {
        public CommandType CommandType { get; private set; }
        public ushort ObjectId { get; private set; }
        public byte Humidity { get; private set; }
        public byte Temperature { get; private set; }
        public byte Insolation { get; private set; }

        public Command(CommandType commandType, ushort objectId, byte humidity, byte temperature, byte insolation)
        {
            CommandType = commandType;
            ObjectId = objectId;
            Humidity = humidity;
            Temperature = temperature;
            Insolation = insolation;
        }
    }
}

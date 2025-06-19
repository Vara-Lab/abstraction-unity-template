
namespace AbstractionServer
{
    public static class Endpoints
    {
        public const string Login = "/auth/login";
        public const string Register = "/auth/register";
        public const string TrafficLightGreen = "/trafficlight/command/green";
        public const string TrafficLightState = "/trafficlight/query/trafficlight";
    }

}
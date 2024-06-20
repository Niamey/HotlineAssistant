using System.Collections.Generic;
using System.Linq;

namespace ContactManager.FunctionalTests.Fixtures
{
    public class CommandEndpoint
    {
        public string Endpoint { get; }

        public object Command { get; }

        public CommandEndpoint(string endpoint, object command)
        {
            Endpoint = endpoint;
            Command = command;
        }

        public static CommandEndpoint For(string endpoint, object command)
        {
            return new CommandEndpoint(endpoint, command);
        }

        public static IEnumerable<object[]> Wrap(params object[] endpoints)
        {
            return endpoints.Select(e => new[] { e });
        }

        public static IEnumerable<object[]> Wrap(params CommandEndpoint[] endpoints)
        {
            return endpoints.Select(t => new[] { t.Endpoint, t.Command });
        }
    }
}
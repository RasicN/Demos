using Shared.Messages.Models;

namespace Shared.Messages
{
    public interface OrderSubmitted
    {
        Order Order { get; set; }
    }
}
using System;
namespace Step2.Services
{
    public interface IWatch
    {
        Action<int> ValueUpdated { get; set; }
        void SendValue(int value);
        void Activate();
        void Deactivate();
    }
}

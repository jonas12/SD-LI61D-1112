using System;

namespace PingPong {
	
    public interface IPingPong {
        bool EstPing { get; set; }
        void Ping(IPingPong pp);
        void Pong();
    }

    public interface IServer {
      void FazPingPong(IPingPong pp);
    }
}

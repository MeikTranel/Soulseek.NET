﻿namespace Soulseek.NET.Messaging.Requests
{ 
    public class PeerTransferRequest
    {
        public PeerTransferRequest(TransferDirection direction, int token, string filename, int size = 0)
        {
            Direction = direction;
            Token = token;
            Filename = filename;
            Size = size;
        }

        public TransferDirection Direction { get; set; }
        public int Token { get; set; }
        public string Filename { get; set; }
        public int Size { get; set; }

        public Message ToMessage()
        {
            return new MessageBuilder()
                .Code(MessageCode.PeerTransferRequest)
                .WriteInteger((int)Direction)
                .WriteInteger(Token)
                .WriteString(Filename)
                .WriteInteger(Size)
                .Build();
        }
    }
}

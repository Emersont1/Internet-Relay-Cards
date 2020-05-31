using System;

namespace LibIRC {
    class Message {
        public String User;
        public String MessageText;

        public Message (String User, String MessageText) {
            this.User = User;
            this.MessageText = MessageText;
        }
    }
}
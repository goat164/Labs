using System;

namespace AdvancedProgramming_Lesson4.Data
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string User { get; set; }
        public string Content { get; set; }
    }
}
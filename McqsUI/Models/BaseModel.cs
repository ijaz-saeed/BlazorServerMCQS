﻿namespace McqsUI.Models
{
    public class BaseModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public BaseModel()
        {
            Name = string.Empty;
            Description = string.Empty;
        }
    }
}

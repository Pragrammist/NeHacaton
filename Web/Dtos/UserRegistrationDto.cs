﻿namespace Web.Dtos
{
    public class InputUserRegistrationDto
    {
        public string Telephone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}

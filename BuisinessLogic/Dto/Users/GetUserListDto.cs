﻿namespace BuisinessLogic.Dto.Users
{
    public class GetUserListDto
    {
        public Guid Id { get; init; }
        
        public string Name { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;
    }
}

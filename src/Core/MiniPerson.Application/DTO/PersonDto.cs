﻿namespace MiniPerson.Application.DTO
{
    public class PersonDto : BaseEntityDto
    {
        #region Props
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationCode { get; set; }
        public string BirthDate { get; set; }
        #endregion
    }
}
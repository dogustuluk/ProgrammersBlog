﻿using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<Category>> Get(int categoryId); //verilerimizi taşımak için oluşturduğumuz resul'ı veriyoruz.
        Task<IDataResult<IList<Category>>> GetAll();
        Task<IResult> Add(CategoryAddDto categoryAddDto, string createdByName); //Dto nedir -> data transfer object olarak geçer. Bunları view model olarak düşünebiliriz. bunlar bizim frontend kısmında sadece ihtiyacımız olan alanları içermektedir.
        Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
        Task<IResult> Delete(int categoryId);
        Task<IResult> HardDelete(int categoryId);

    }
}

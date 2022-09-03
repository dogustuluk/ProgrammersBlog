using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Utilities;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CommentDto>> AddAsync(CommentAddDto commentAddDto)
        {
            var comment = _mapper.Map<Comment>(commentAddDto);
            var addedComment = await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveAsync();
            return new DataResult<CommentDto>(ResultStatus.Success, Messages.Comment.Add(comment.CreatedByName), new CommentDto
            {
                Comment = addedComment,
            });
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var commentsCount = await _unitOfWork.Comments.CountAsync();
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir hata ile karşılaşıldı", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var commentsCount = await _unitOfWork.Comments.CountAsync(c => !c.IsDeleted);
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStatus.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, "Beklenmeyen bir hata ile karşılaşıldı", -1);
            }
        }

        public async Task<IDataResult<CommentDto>> DeleteAsync(int commentId, string modifiedByName)
        {
            var comment = await _unitOfWork.Comments.GetAsync(c => c.Id == commentId);
            if (comment != null)
            {
                comment.IsDeleted = true;
                comment.ModifiedByName = modifiedByName;
                comment.ModifiedDate = DateTime.Now;
                var deletedComment = await _unitOfWork.Comments.UpdateAsync(comment);
                await _unitOfWork.SaveAsync();
                return new DataResult<CommentDto>(ResultStatus.Success, Messages.Comment.Delete(deletedComment.CreatedByName), new CommentDto
                {
                    Comment = deletedComment,
                });
            }
            return new DataResult<CommentDto>(ResultStatus.Error, Messages.Comment.NotFound(isPlural: false), new CommentDto
            {
                Comment = null,
            });
        }

        public Task<IDataResult<CommentListDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<CommentListDto>> GetAllBtNonDeletedAndActiveAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<CommentListDto>> GetAllByDeletedAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<CommentListDto>> GetAllByNonDeletedAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<CommentDto>> GetAsync(int commentId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<CommentUpdateDto>> GetCommentUpdateDtoAsync(int commentId)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> HardDeleteAsync(int commentId)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<CommentDto>> UpdateAsync(CommentUpdateDto commentUpdateDto, string modifiedByName)
        {
            throw new NotImplementedException();
        }
    }
}

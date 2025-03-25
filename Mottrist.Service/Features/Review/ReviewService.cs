using Mottrist.Domain.Common.IUnitOfWork;
using Mottrist.Domain.Global;
using Mottrist.Service.Features.General;
//using Mottrist.Service.Features.General.Log;
using Mottrist.Service.Features.Review.Inerfaces;

namespace Mottrist.Service.Features.Review
{
    public class ReviewService : BaseService, IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        //private ILog _logger
        ///, ILog logger
        public ReviewService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            //_logger = logger;
        }


     
    }
}

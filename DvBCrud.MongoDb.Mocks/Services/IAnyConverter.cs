using DvBCrud.Common.Services.Conversion;
using DvBCrud.MongoDB.Mocks.Models;

namespace DvBCrud.MongoDB.Mocks.Services;

public interface IAnyConverter : IConverter<AnyDataModel, AnyApiModel>
{
    
}
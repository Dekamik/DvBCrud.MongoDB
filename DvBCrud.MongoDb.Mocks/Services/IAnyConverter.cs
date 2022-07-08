using DvBCrud.Common.Services.Conversion;
using DvBCrud.MongoDb.Mocks.Models;

namespace DvBCrud.MongoDb.Mocks.Services;

public interface IAnyConverter : IConverter<AnyDataModel, AnyApiModel>
{
    
}
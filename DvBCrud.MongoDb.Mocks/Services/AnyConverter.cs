using DvBCrud.MongoDb.Mocks.Models;

namespace DvBCrud.MongoDb.Mocks.Services;

public class AnyConverter : IAnyConverter
{
    public AnyApiModel ToApiModel(AnyDataModel dataModel) =>
        new()
        {
            AnyString = dataModel.AnyString
        };

    public AnyDataModel ToDataModel(AnyApiModel apiModel) =>
        new()
        {
            AnyString = apiModel.AnyString
        };
}
using DvBCrud.MongoDB.Mocks.Models;

namespace DvBCrud.MongoDB.Mocks.Services;

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
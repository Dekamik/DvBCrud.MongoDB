using System;
using System.Linq;
using System.Threading.Tasks;
using DvBCrud.MongoDb.Mocks.Models;
using DvBCrud.MongoDb.Mocks.Repositories;
using DvBCrud.MongoDb.Mocks.Services;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace DvBCrud.MongoDb.Services.UnitTests;

public class ServiceTests
{
    private readonly IAnyRepository _repository;
    private readonly IAnyConverter _converter;
    private readonly AnyService _service;

    public ServiceTests()
    {
        _repository = A.Fake<IAnyRepository>();
        _converter = A.Fake<IAnyConverter>();
        _service = new AnyService(_repository, _converter);
    }

    [Fact]
    public void GetAll_Any_CallsFindAndConverter()
    {
        var data = new[]
        {
            new AnyDataModel
            {
                Id = "AnyId1",
                AnyString = "AnyString"
            },
            new AnyDataModel
            {
                Id = "AnyId2",
                AnyString = "AnyStr"
            }
        };

        var models = new[]
        {
            new AnyApiModel
            {
                AnyString = "AnyString"
            },
            new AnyApiModel
            {
                AnyString = "AnyStr"
            }
        };

        A.CallTo(() => _repository.Find())
            .Returns(data);
        
        for (var i = 0; i < data.Length; i++)
        {
            var index = i;
            A.CallTo(() => _converter.ToApiModel(data[index]))
                .Returns(models[index]);
        }

        var results = _service.GetAll().ToArray();

        A.CallTo(() => _repository.Find())
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => _converter.ToApiModel(A<AnyDataModel>._))
            .MustHaveHappenedTwiceExactly();

        for (var i = 0; i < results.Length; i++)
        {
            results[i].AnyString
                .Should()
                .Be(models[i].AnyString);
        }
    }
    
    [Fact]
    public async Task GetAllAsync_Any_CallsFindAsyncAndConverter()
    {
        var data = new[]
        {
            new AnyDataModel
            {
                Id = "AnyId1",
                AnyString = "AnyString"
            },
            new AnyDataModel
            {
                Id = "AnyId2",
                AnyString = "AnyStr"
            }
        };

        var models = new[]
        {
            new AnyApiModel
            {
                AnyString = "AnyString"
            },
            new AnyApiModel
            {
                AnyString = "AnyStr"
            }
        };

        A.CallTo(() => _repository.FindAsync())
            .Returns(data);
        
        for (var i = 0; i < data.Length; i++)
        {
            var index = i;
            A.CallTo(() => _converter.ToApiModel(data[index]))
                .Returns(models[index]);
        }

        var results = (await _service.GetAllAsync()).ToArray();

        A.CallTo(() => _repository.FindAsync())
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => _converter.ToApiModel(A<AnyDataModel>._))
            .MustHaveHappenedTwiceExactly();

        for (var i = 0; i < results.Length; i++)
        {
            results[i].AnyString
                .Should()
                .Be(models[i].AnyString);
        }
    }

    [Fact]
    public void Get_Any_CallsFindWithIdAndConverter()
    {
        const string id = "AnyId";
        var dataModel = new AnyDataModel();
        var apiModel = new AnyApiModel();

        A.CallTo(() => _repository.Find(id))
            .Returns(dataModel);
        A.CallTo(() => _converter.ToApiModel(dataModel))
            .Returns(apiModel);

        var actual = _service.Get(id);

        actual.Should().Be(apiModel);
    }

    [Fact]
    public void Get_NullId_ThrowsArgumentNullException()
    {
        _service.Invoking(x => x.Get(null))
            .Should()
            .Throw<ArgumentNullException>();
    }

    [Fact]
    public async Task GetAsync_Any_CallsFindAsyncWithIdAndConverter()
    {
        const string id = "AnyId";
        var dataModel = new AnyDataModel();
        var apiModel = new AnyApiModel();

        A.CallTo(() => _repository.FindAsync(id))
            .Returns(dataModel);
        A.CallTo(() => _converter.ToApiModel(dataModel))
            .Returns(apiModel);

        var actual = await _service.GetAsync(id);

        actual.Should().Be(apiModel);
    }

    [Fact]
    public void GetAsync_NullId_ThrowsArgumentNullException()
    {
        _service.Awaiting(x => x.GetAsync(null))
            .Should()
            .Throw<ArgumentNullException>();
    }

    [Fact]
    public void Create_Any_ConvertsAndCreates()
    {
        var apiModel = new AnyApiModel();
        var dataModel = new AnyDataModel();

        A.CallTo(() => _converter.ToDataModel(apiModel))
            .Returns(dataModel);
        
        _service.Create(apiModel);

        A.CallTo(() => _repository.Create(dataModel))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Create_NullApiModel_ThrowsArgumentNullException()
    {
        _service.Invoking(x => x.Create(null))
            .Should()
            .Throw<ArgumentNullException>();
    }
    
    [Fact]
    public async Task CreateAsync_Any_ConvertsAndCreatesAsynchronously()
    {
        var apiModel = new AnyApiModel();
        var dataModel = new AnyDataModel();

        A.CallTo(() => _converter.ToDataModel(apiModel))
            .Returns(dataModel);
        
        await _service.CreateAsync(apiModel);

        A.CallTo(() => _repository.CreateAsync(dataModel))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void CreateAsync_NullApiModel_ThrowsArgumentNullException()
    {
        _service.Awaiting(x => x.CreateAsync(null))
            .Should()
            .Throw<ArgumentNullException>();
    }
    
    [Fact]
    public void Update_Any_ConvertsAndUpdates()
    {
        const string id = "AnyId";
        var apiModel = new AnyApiModel();
        var dataModel = new AnyDataModel();

        A.CallTo(() => _converter.ToDataModel(apiModel))
            .Returns(dataModel);
        
        _service.Update(id, apiModel);

        A.CallTo(() => _repository.Update(id, dataModel))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public void Update_NullId_ThrowsArgumentNullException()
    {
        _service.Invoking(x => x.Update(null, new AnyApiModel()))
            .Should()
            .Throw<ArgumentNullException>();
    }
    
    [Fact]
    public void Update_NullApiModel_ThrowsArgumentNullException()
    {
        _service.Invoking(x => x.Update("AnyId", null))
            .Should()
            .Throw<ArgumentNullException>();
    }
    
    [Fact]
    public async Task UpdateAsync_Any_ConvertsAndUpdatesAsynchronously()
    {
        const string id = "AnyId";
        var apiModel = new AnyApiModel();
        var dataModel = new AnyDataModel();

        A.CallTo(() => _converter.ToDataModel(apiModel))
            .Returns(dataModel);
        
        await _service.UpdateAsync(id, apiModel);

        A.CallTo(() => _repository.UpdateAsync(id, dataModel))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public void UpdateAsync_NullId_ThrowsArgumentNullException()
    {
        _service.Awaiting(x => x.UpdateAsync(null, new AnyApiModel()))
            .Should()
            .Throw<ArgumentNullException>();
    }
    
    [Fact]
    public void UpdateAsync_NullApiModel_ThrowsArgumentNullException()
    {
        _service.Awaiting(x => x.UpdateAsync("AnyId", null))
            .Should()
            .Throw<ArgumentNullException>();
    }

    [Fact]
    public void Delete_Any_CallsRemove()
    {
        const string id = "AnyId";
        
        _service.Delete(id);

        A.CallTo(() => _repository.Remove(id))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void Delete_NullId_ThrowsArgumentNullException()
    {
        _service.Invoking(x => x.Delete(null))
            .Should()
            .Throw<ArgumentNullException>();
    }
    
    [Fact]
    public async Task DeleteAsync_Any_CallsRemove()
    {
        const string id = "AnyId";
        
        await _service.DeleteAsync(id);

        A.CallTo(() => _repository.RemoveAsync(id))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void DeleteAsync_NullId_ThrowsArgumentNullException()
    {
        _service.Invoking(x => x.DeleteAsync(null))
            .Should()
            .Throw<ArgumentNullException>();
    }
}
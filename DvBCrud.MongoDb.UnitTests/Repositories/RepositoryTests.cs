using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DvBCrud.MongoDB.Mocks.Models;
using DvBCrud.MongoDB.Mocks.Repositories;
using DvBCrud.MongoDB.Repositories.Wrappers;
using FakeItEasy;
using FluentAssertions;
using MongoDB.Driver;
using Xunit;

namespace DvBCrud.MongoDB.Tests.Repositories
{
    public class RepositoryTests
    {
        private readonly IMongoCollectionWrapper<AnyModel> _collection;
        private readonly AnyRepository _repository;

        public RepositoryTests()
        {
            var client = A.Fake<IMongoClient>();
            var factory = A.Fake<IMongoCollectionWrapperFactory>();
            _collection = A.Fake<IMongoCollectionWrapper<AnyModel>>();
            
            A.CallTo(() => factory.Create(A<IMongoCollection<AnyModel>>._))
                .Returns(_collection);

            _repository = new AnyRepository(client, factory, "AnyDb");
        }

        [Fact]
        public void Find_NoArgument_FindCalled()
        {
            _repository.Find();

            A.CallTo(() => _collection.Find(A<Expression<Func<AnyModel, bool>>>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Find_WithId_FindCalled()
        {
            const string id = "AnyId";

            _repository.Find(id);

            A.CallTo(() => _collection.Find(A<Expression<Func<AnyModel, bool>>>.Ignored))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Find_WithArray_FindCalled()
        {
            var array = new []
            {
                "AnyId",
                "AnyId2"
            };

            _repository.Find(array);

            A.CallTo(() => _collection.Find(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Find_WithArrayContainingSingleItem_FindWithIdCalled()
        {
            var array = new []
            {
                "AnyId"
            };

            _repository.Find(array);

            A.CallTo(() => _collection.Find(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Find_WithNullArray_ThrowsArgumentNullException()
        {
            _repository.Invoking(r => r.Find(ids: null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Find_WithEmptyArray_ThrowsArgumentException()
        {
            _repository.Invoking(r => r.Find(new string[] { })).Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public void Find_MissingId_ThrowsArgumentNullException()
        {
            _repository.Invoking(r => r.Find(id: null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task FindAsync_NoArgument_FindAsyncCalled()
        {
            await _repository.FindAsync();

            A.CallTo(() => _collection.FindAsync(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task FindAsync_WithId_FindAsyncCalled()
        {
            const string id = "AnyId";

            await _repository.FindAsync(id);

            A.CallTo(() => _collection.FindAsync(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void FindAsync_MissingId_ThrowsArgumentNullException()
        {
            _repository.Awaiting(r => r.FindAsync(id: null)).Should().Throw<ArgumentNullException>();
        }
        
        [Fact]
        public async Task FindAsync_WithArray_FindCalled()
        {
            var array = new []
            {
                "AnyId",
                "AnyId2"
            };

            await _repository.FindAsync(array);

            A.CallTo(() => _collection.FindAsync(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task FindAsync_WithArrayContainingSingleItem_FindWithIdCalled()
        {
            var array = new []
            {
                "AnyId"
            };

            await _repository.FindAsync(array);

            A.CallTo(() => _collection.FindAsync(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void FindAsync_WithNullArray_ThrowsArgumentNullException()
        {
            _repository.Awaiting(r => r.FindAsync(ids: null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void FindAsync_WithEmptyArray_ThrowsArgumentException()
        {
            _repository.Awaiting(r => r.FindAsync(new string[] { })).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Create_Any_InsertOneCalled()
        {
            // Arrange
            var model = new AnyModel
            {
                AnyString = "AnyString"
            };

            // Act
            _repository.Create(model);

            // Assert
            A.CallTo(() => _collection.InsertOne(model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Create_Multiple_InsertManyCalled()
        {
            // Arrange
            var models = new[]
            {
                new AnyModel
                {
                    AnyString = "AnyString"
                }
            };

            // Act
            _repository.Create(models);

            // Assert
            A.CallTo(() => _collection.InsertMany(models)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void CreateAsync_Any_InsertOneCalled()
        {
            // Arrange
            var model = new AnyModel
            {
                AnyString = "AnyString"
            };

            // Act
            _repository.CreateAsync(model);

            // Assert
            A.CallTo(() => _collection.InsertOneAsync(model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void CreateAsync_Multiple_InsertManyCalled()
        {
            // Arrange
            var models = new[]
            {
                new AnyModel
                {
                    AnyString = "AnyString"
                }
            };

            // Act
            _repository.CreateAsync(models);

            // Assert
            A.CallTo(() => _collection.InsertManyAsync(models)).MustHaveHappenedOnceExactly();
        }
        
        [Fact]
        public void Update_Any_ReplaceOneCalled()
        {
            const string id = "AnyId";
            var model = new AnyModel
            {
                AnyString = "AnyString"
            };

            _repository.Update(id, model);

            A.CallTo(() => _collection.ReplaceOne(A<Expression<Func<AnyModel, bool>>>.Ignored, model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Update_MissingId_ThrowsArgumentNullException()
        {
            _repository.Invoking(r => r.Update(null, new AnyModel())).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UpdateAsync_Any_ReplaceOneAsyncCalled()
        {
            const string id = "AnyId";
            var model = new AnyModel
            {
                AnyString = "AnyString"
            };

            _repository.UpdateAsync(id, model);

            A.CallTo(() => _collection.ReplaceOneAsync(A<Expression<Func<AnyModel, bool>>>.Ignored, model)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void UpdateAsync_MissingId_ThrowsArgumentNullException()
        {
            _repository.Awaiting(r => r.UpdateAsync(null, new AnyModel())).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Remove_AnyId_DeleteOneCalled()
        {
            const string id = "AnyId";

            _repository.Remove(id);

            A.CallTo(() => _collection.DeleteOne(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Remove_MissingId_ThrowsArgumentNullException()
        {
            _repository.Invoking(r => r.Remove(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void RemoveAsync_AnyId_DeleteOneAsyncCalled()
        {
            const string id = "AnyId";

            _repository.RemoveAsync(id);

            A.CallTo(() => _collection.DeleteOneAsync(A<Expression<Func<AnyModel, bool>>>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void RemoveAsync_MissingId_ThrowsArgumentNullException()
        {
            _repository.Invoking(r => r.RemoveAsync(null)).Should().Throw<ArgumentNullException>();
        }
    }
}

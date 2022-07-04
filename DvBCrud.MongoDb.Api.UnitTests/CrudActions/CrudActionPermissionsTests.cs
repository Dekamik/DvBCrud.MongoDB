using DvBCrud.MongoDB.API.CrudActions;
using FluentAssertions;
using Xunit;

namespace DvBCrud.MongoDB.API.UnitTests.CrudActions
{
    public class CrudActionPermissionsTests
    {
        [Fact]
        public void IsActionAllowed_AllowedActionsNotDefined_AllActionsAllowed()
        {
            var crudActions = new CrudActionPermissions();
            crudActions.IsActionAllowed(CrudAction.Create).Should().BeTrue();
            crudActions.IsActionAllowed(CrudAction.Read).Should().BeTrue();
            crudActions.IsActionAllowed(CrudAction.Update).Should().BeTrue();
            crudActions.IsActionAllowed(CrudAction.Delete).Should().BeTrue();
        }

        [Fact]
        public void IsActionAllowed_ReadOnlyActions_OnlyReadAllowed()
        {
            var crudActions = new CrudActionPermissions(CrudAction.Read);
            crudActions.IsActionAllowed(CrudAction.Create).Should().BeFalse();
            crudActions.IsActionAllowed(CrudAction.Read).Should().BeTrue();
            crudActions.IsActionAllowed(CrudAction.Update).Should().BeFalse();
            crudActions.IsActionAllowed(CrudAction.Delete).Should().BeFalse();
        }

        [Fact]
        public void IsActionAllowed_NoDelete_OnlyDeleteForbidden()
        {
            var crudActions = new CrudActionPermissions(CrudAction.Create, CrudAction.Read, CrudAction.Update);
            crudActions.IsActionAllowed(CrudAction.Create).Should().BeTrue();
            crudActions.IsActionAllowed(CrudAction.Read).Should().BeTrue();
            crudActions.IsActionAllowed(CrudAction.Update).Should().BeTrue();
            crudActions.IsActionAllowed(CrudAction.Delete).Should().BeFalse();
        }
    }
}

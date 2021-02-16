using DvBCrud.MongoDB.API.Actions;
using DvBCrud.MongoDB.API.XMLJSON;
using FluentAssertions;
using Xunit;

namespace DvBCrud.MongoDB.API.Tests.Actions
{
    public class ActionRestrictionsTests
    {
        [Fact]
        public void IsActionAllowed_AllowedActionsNotDefined_AllActionsAllowed()
        {
            var crudActions = new ActionRestrictions();
            crudActions.IsActionAllowed(CRUDAction.Create).Should().BeTrue();
            crudActions.IsActionAllowed(CRUDAction.Read).Should().BeTrue();
            crudActions.IsActionAllowed(CRUDAction.Update).Should().BeTrue();
            crudActions.IsActionAllowed(CRUDAction.Delete).Should().BeTrue();
        }

        [Fact]
        public void IsActionAllowed_ReadOnlyActions_OnlyReadAllowed()
        {
            var crudActions = new ActionRestrictions(CRUDAction.Read);
            crudActions.IsActionAllowed(CRUDAction.Create).Should().BeFalse();
            crudActions.IsActionAllowed(CRUDAction.Read).Should().BeTrue();
            crudActions.IsActionAllowed(CRUDAction.Update).Should().BeFalse();
            crudActions.IsActionAllowed(CRUDAction.Delete).Should().BeFalse();
        }

        [Fact]
        public void IsActionAllowed_NoDelete_OnlyDeleteForbidden()
        {
            var crudActions = new ActionRestrictions(CRUDAction.Create, CRUDAction.Read, CRUDAction.Update);
            crudActions.IsActionAllowed(CRUDAction.Create).Should().BeTrue();
            crudActions.IsActionAllowed(CRUDAction.Read).Should().BeTrue();
            crudActions.IsActionAllowed(CRUDAction.Update).Should().BeTrue();
            crudActions.IsActionAllowed(CRUDAction.Delete).Should().BeFalse();
        }
    }
}

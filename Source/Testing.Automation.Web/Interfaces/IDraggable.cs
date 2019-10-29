namespace Testing.Automation.Web.Interfaces
{
    public interface IDraggable : IHasBackingElement
    {
        IPerformsDragAndDrop GetDragAndDropPerformer();
    }
}
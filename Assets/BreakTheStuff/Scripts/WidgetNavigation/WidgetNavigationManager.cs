using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WidgetNavigationManager : MonoBehaviour {

    [SerializeField] ContentController mainWidget;
    [SerializeField] List<ContentController> widgetList;

    private string oldWidgetName;

    public void PushWidget(string name)
    {
        this.oldWidgetName = name;

        var widget = widgetList.Where(x => x.Name == name).SingleOrDefault();

        if(widget != null)
        {
            if (!widget.DontHideMainWidget)
                mainWidget.Hide();

            widget.Show();
        }
    }

    public void PopWidget()
    {
        var widget = widgetList.Where(x => x.Name == oldWidgetName).SingleOrDefault();

        if (widget != null)
        {
            if (!widget.DontHideMainWidget)
                mainWidget.Show();

            widget.Hide();
        }
    }
}

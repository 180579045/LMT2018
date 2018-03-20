# SCMT用户控件库说明

## 窗口布局
### MetroUI

### Avalon
Avalon的基础层级关系:
```
<avalon:DockingManager x:Name="dockingManager">
    <avalon:LayoutRoot>
        <avalon:LayoutPanel Orientation="Horizontal">
            <avalon:LayoutDocumentPane>
                <avalon:LayoutAnchorable Title="Sample Tool Pane">

                </avalon:LayoutAnchorable>
            </avalon:LayoutDocumentPane>
        </avalon:LayoutPanel>
    </avalon:LayoutRoot>
</avalon:DockingManager>
```

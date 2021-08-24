import {
  Component,
  Input,
  OnInit,
  AfterViewInit,
    AfterViewChecked, ViewChild
} from '@angular/core';
import 'devextreme/data/odata/store';
import { TreeService } from './tree.service';

import { Employee, VI } from './../../models/employee.model';
import { concat } from 'rxjs';
import { DxDataGridComponent, DxTreeListComponent } from 'devextreme-angular';
import { hideColumn, toCapitalizeWord, colorIcons } from './helpers';
// import { Employee } from './../../models/employee.model';
declare var $: any;

@Component({
  selector: 'app-tree',
  templateUrl: 'tree.component.html',
  styleUrls: ['tree.component.scss'],
})
export class TreeComponent implements AfterViewChecked {
  @Input() showBorders = true;
  @Input() showRowLines = true;
  @Input() isInfoColumnVisible = true;
  @Input() showSearchFied = true;
  @Input() allowEdit = true;
  @Input() allowDelete = true;
  @Input() allowpagination = true;
  @Input() allowReordering = true;
  @Input() parentIdField = 'Head_ID';
  @Input() columnIdName = 'ID';
  @Input() viewID = '1';
  @Input() hideColumns = ['ID', 'Head ID'];
  @Input() sortMode = 'multiple';
  @Input() iconsColor = '#ff5722';
  @Input() editColor = '#ff5722';
  @Input() deleteColor = '#ff5722';
  @Input() expandedRowKeys: Array<number> = [0];
@Input() data:any=[]
  //employees: Array<Employee>;

  allowDropInsideItem = true;
  showDragIcons = true;

  @ViewChild('dataGridVar', { static: false }) dataGrid: DxTreeListComponent;

  constructor(public treeService: TreeService) {

  // if(this.data==[])
  // {
  //  // this.data = treeService.getEmployees();
  //   treeService.GetData(this.treeService.ViewID,"en").subscribe(resp => {
  //     debugger;
  //     this.data=resp;      
  //   })
  // }
    this.onReorder = this.onReorder.bind(this);
  }

  ngOnInit(): void {
   // this.data=this.treeService.getVI();
   // this.data = treeService.getEmployees();
    this.treeService.GetData(this.treeService.ViewID,"en").subscribe(resp => {
      this.data=resp;      
    })

    this.dataGrid.instance.refresh()

    }



    ngAfterViewChecked(): void {
        // hide columns from table by column name
        if (this.hideColumns.length > 0) {
            for (let i = 0; i < this.hideColumns.length; i++) {
                let columnName = toCapitalizeWord(this.hideColumns[i]);
                hideColumn(columnName);
            }
        }
        // color the icons of actions
        colorIcons(
            this.allowDelete,
            this.allowEdit,
            this.iconsColor,
            this.editColor,
            this.deleteColor
        );
        }
  onDragChange(e: any) {
    let visibleRows = e.component.getVisibleRows(),
      sourceNode = e.component.getNodeByKey(e.itemData[this.columnIdName]),
      targetNode = visibleRows[e.toIndex].node;

    while (targetNode && targetNode.data) {
      if (
        targetNode.data[this.columnIdName] ===
        sourceNode.data[this.columnIdName]
      ) {
        e.cancel = true;
        break;
      }
      targetNode = targetNode.parent;
    }
   }

  ngOnChanges(e: any) {
    console.log(this.data);
    this.data=this.treeService.getVI();
  //   debugger;
   this.treeService.GetData(e.viewID.currentValue,"en").subscribe(resp => {
   this.data=resp;   
    
    console.log(this.data)
    this.dataGrid.instance.refresh()
  })
  }

  onReorder(e: any) {
    let visibleRows = e.component.getVisibleRows(),
      sourceData = e.itemData,
      targetData = visibleRows[e.toIndex].node.data;

    if (e.dropInsideItem) {
      e.itemData[this.parentIdField] = targetData[this.columnIdName];
      e.component.refresh();
    } else {
      let sourceIndex = this.data.indexOf(sourceData),
        targetIndex = this.data.indexOf(targetData);

      if (sourceData[this.parentIdField] !== targetData[this.parentIdField]) {
        sourceData[this.parentIdField] = targetData[this.parentIdField];
        if (e.toIndex > e.fromIndex) {
          targetIndex++;
        }
      }

      this.data.splice(sourceIndex, 1);
      this.data.splice(targetIndex, 0, sourceData);
    }
  }
}

// preserveWhitespaces: true

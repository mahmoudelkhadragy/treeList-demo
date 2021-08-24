import { Component } from '@angular/core';
import { TreeService } from './../tree/tree.service';
import { Employee, VI } from '../../models/employee.model';
declare var $: any;
@Component({
  templateUrl: 'home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  showBorders:boolean = true;
  showRowLines:boolean = true;
  showSearchField:boolean=true;
  isActionColumnVisible:boolean=true;
  ViewID:string="1";
  allowEdit:boolean =true;
  AllowDelete:boolean=true;
  datasource:VI[]=[]

  constructor(public treeService:TreeService) {
    debugger;
this.datasource=this.treeService.getVI();

    
  }

  public showhideBorders() {
    this.showBorders = !this.showBorders;
  }

  showHideRowLines() {
    this.showRowLines = !this.showRowLines;
  }
  showHideSearchField() {
    this.showSearchField = !this.showSearchField;
  }

  ShowHideActionColumn() {
    this.isActionColumnVisible = !this.isActionColumnVisible;
  }

showHideEdit(){
  debugger;
  this.allowEdit=!this.allowEdit;
}

showHideDelete(){
  debugger;
  this.AllowDelete=!this.AllowDelete;
}

ChangeColor($event:any,colorValue:string){
  debugger;
    $('table').css('color', colorValue);
}

ChangeView($event:any,ViewID:string){
this.treeService.ViewID =ViewID;

}

}

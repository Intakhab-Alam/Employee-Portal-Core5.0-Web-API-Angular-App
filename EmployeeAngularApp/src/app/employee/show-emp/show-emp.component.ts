import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-show-emp',
  templateUrl: './show-emp.component.html',
  styleUrls: ['./show-emp.component.scss']
})
export class ShowEmpComponent implements OnInit {

  EmployeeList: any=[];

  ModalTitle: string='';
  ActivateAddEditEmpComp: boolean=false;
  emp: any;

  constructor(private service: SharedService) { }

  ngOnInit(): void {
    this.refreshEmpList();
  }

  addClick() {
    this.emp={
      EmployeeId:0,
      EmployeeName:'',
      Department:'',
      DateOfJoining:'',
      PhotoFileName:'anonymous.png'
    }
    this.ModalTitle="Add Employee";
    this.ActivateAddEditEmpComp=true;
  }

  editClick(item: any) {
    this.emp=item;
    this.ModalTitle="Edit Employee";
    this.ActivateAddEditEmpComp=true;
  }

  deleteClick(item: any) {
    if(confirm('Are you sure??')) {
      this.service.deleteEmployee(item.EmployeeId).subscribe(res => {
          alert(res.toString());
          this.refreshEmpList();
      })
    }
  }

  closeClick() {
    this.ActivateAddEditEmpComp=false;
    this.refreshEmpList();
  }

  refreshEmpList() {
    this.service.getEmployeeList().subscribe(data => {
      this.EmployeeList = data;
    });  
  }

}

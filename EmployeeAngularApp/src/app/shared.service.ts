import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  readonly APIUrl = "https://localhost:44399/api";
  readonly PhotoUrl = "https://localhost:44399/Photos/";

  // json-server db.json for fake api
  //readonly APIUrl = "http://localhost:3000";

  constructor(private http: HttpClient) { }

  //Department
  getDepList(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl+"/department");
  }

  addDepartment(val: any) {
    return this.http.post(this.APIUrl+'/department', val);
  }

  updateDepartment(val: any) {
    return this.http.put(this.APIUrl+'/department', val);
  }

  deleteDepartment(id: any) {
    return this.http.delete(this.APIUrl+'/department/'+id);
  }

  //Employee
  getEmployeeList(): Observable<any[]> {
    return this.http.get<any>(this.APIUrl+"/employee");
  }

  addEmployee(val: any) {
    return this.http.post(this.APIUrl+'/employee', val);
  }

  updateEmployee(val: any) {
    return this.http.put(this.APIUrl+'/employee', val);
  }

  deleteEmployee(id: any) {
    return this.http.delete(this.APIUrl+'/employee/'+id);
  }

  uploadPhoto(val: any) {
    return this.http.post(this.APIUrl+'/employee/savefile', val);
  }
  
  getAllDepartmentNames(): Observable<any[]> {
    return this.http.get<any[]>(this.APIUrl+"/employee/GetAllDepartmentNames");
  }
}

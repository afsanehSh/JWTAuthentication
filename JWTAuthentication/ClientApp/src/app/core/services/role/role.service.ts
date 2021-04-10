import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RoleModel } from '../../models/role.model';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

   constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  getRoleList() {
    return this.http.get(this.baseUrl + 'Role');
  }
  getRoleById(id: number) {
    return this.http.get(this.baseUrl + 'Role/' + id);
  }
  CreateRole(role: RoleModel) {
    return this.http.post(this.baseUrl + 'Role', role);
  }
  updateRole(role: RoleModel) {
    return this.http.put(this.baseUrl + 'Role', role);
  }

  deleteRole(id: number) {
    return this.http.delete(this.baseUrl + 'Role/' + id);
  }

}

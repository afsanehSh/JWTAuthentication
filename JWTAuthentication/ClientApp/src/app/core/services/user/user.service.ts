import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserModel } from '../../models/user.model';
import { PasswordModel } from '../../models/change-password.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  changePassword(passwordModel: PasswordModel) {
    return this.http.put(this.baseUrl + 'AppUser/ChangePassword', passwordModel);
  }

  getUserList() {
    return this.http.get(this.baseUrl + 'AppUser');
  }
  getUserById(id: number) {
    return this.http.get(this.baseUrl + 'AppUser/' + id);
  }
  CreateUser(user: UserModel) {
    return this.http.post(this.baseUrl + 'AppUser', user);
  }
  updateUser(user: UserModel) {
    return this.http.put(this.baseUrl + 'AppUser', user);
  }

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + 'AppUser/' + id);
  }
}

import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { UserModel } from '../models/user.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private currentUserSubject: BehaviorSubject<UserModel>;
  public currentUser: Observable<UserModel>;

  constructor(private http: HttpClient,
    private router: Router,
    @Inject('BASE_URL') private baseUrl: string) {

    this.currentUserSubject = new BehaviorSubject<UserModel>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): UserModel {
    return this.currentUserSubject.value;
  }

  // Authentication/Authorization
  login(user: { userName: string, password: string }): Observable<UserModel> {
    if (localStorage.getItem('currentUser'))
      localStorage.removeItem('currentUser');
    if (localStorage.getItem('layoutConfigV702'))
      localStorage.removeItem('layoutConfigV702');
    return this.http.post<UserModel>(this.baseUrl + 'Authentication/SignInByUserName', user)
      .pipe(map(user => {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      }));
  }

  //register(user: UserModel): Observable<any> {
  //  let httpHeaders = new HttpHeaders();
  //  httpHeaders = httpHeaders.set('Content-Type', 'application/json');
  //  return this.http.post<UserModel>(API_USERS_URL, user, { headers: httpHeaders })
  //    .pipe(
  //      map((res: UserModel) => {
  //        return res;
  //      }),
  //      catchError(err => {
  //        return null;
  //      })
  //    );
  //}


  logout() {
    localStorage.removeItem('currentUser');
    this.router.navigate(['/auth/login'], {
      queryParams: {},
    });
  }
}

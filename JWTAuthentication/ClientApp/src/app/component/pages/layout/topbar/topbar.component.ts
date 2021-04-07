import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { UserModel } from '../../../../core/models/user.model';
import { AuthService } from '../../../../core/services/auth.service';

@Component({
  selector: 'app-topbar',
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.css']
})
export class TopbarComponent implements OnInit {

  currentUser: UserModel;

  constructor(private auth: AuthService,
    private cdr: ChangeDetectorRef) {

    this.currentUser = this.auth.currentUserValue;
  }

  ngOnInit() {
  }

}

import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { UserService } from '../shared/services/user.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
    standalone: false
})
export class HomeComponent implements OnInit {

  status: boolean = false;
  subscription?: Subscription;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.subscription = this.userService.authStatus$.subscribe(status => this.status = status);
  }

}

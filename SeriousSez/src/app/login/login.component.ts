import { Subscription } from 'rxjs';
import { Component, OnInit,OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { Credentials } from '../shared/models/credentials.interface';
import { UserService } from '../shared/services/user.service';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  private subscription: Subscription;
  public loginForm: FormGroup;

  brandNew: boolean = false;
  errors: string = "";
  isRequesting: boolean = false;;
  submitted: boolean = false;
  credentials: Credentials = { identity: '', password: '' };

  constructor(private userService: UserService, private router: Router,private activatedRoute: ActivatedRoute, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      identity: ['', Validators.required],
      password: ['', Validators.required]
    });
    
  // subscribe to router event
  this.subscription = this.activatedRoute.queryParams.subscribe(
    (param: any) => {
        this.brandNew = param['brandNew'];   
        this.credentials.identity = param['identity'];         
    });      
  }

  ngOnDestroy() {
    // prevent memory leak by unsubscribing
    this.subscription.unsubscribe();
  }

  login({ value, valid }: { value: Credentials, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors='';

    if (valid) {
      this.userService.login(value)
        .subscribe(result => {
          this.router.navigate(['recipes/overview'],{ queryParams: { brandNew: true, email: value.identity }});              
          this.isRequesting = false;           
        }, errors => {
          this.isRequesting = false;
          this.errors = errors.error.Item2;
        }
      );
    }
  }

  get f(): { [key: string]: AbstractControl } {
    return this.loginForm.controls;
  }
}
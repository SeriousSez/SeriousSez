import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserRegistration } from '../../models/user.registration.interface';
import { UserService } from '../../services/user.service';
import { User } from 'src/app/account/models/user.interface';
import { validate } from 'uuid';

@Component({
  selector: 'app-registration-modal',
  templateUrl: './registration.modal.html',
  styleUrls: ['./registration.modal.css']
})
export class RegistrationModal implements OnInit {

    @Input() title: string = "Create your account";
    @Input() confirmButton: string = "Sign Up";
    @Input() card: boolean;
    @Input() baseUrl: string = "/account/create";
    @Input() navigationUrl: string = "login";
    @Input() roles: string[];

    @Output() finish = new EventEmitter();

    public errors: string = '';
    public isRequesting: boolean = false;
    public submitted: boolean = false;
    public registerForm: FormGroup;
    public selectedRole: string;
    
    constructor(private userService: UserService, private router: Router, private formBuilder: FormBuilder) { }
  
    ngOnInit() {
      this.registerForm = this.formBuilder.group({
        username: ['', Validators.required],
        email: ['', [Validators.required, Validators.email]],
        firstName: ['', Validators.required],
        lastName: ['',],
        password: ['', Validators.required],
        role: ['User', Validators.required]
      });
    }
  
    register({ value, valid }: { value: UserRegistration, valid: boolean }) {
        this.submitted = true;
        this.isRequesting = true;
        this.errors='';
        
        if (valid){
            this.userService.register(value, this.baseUrl)
            .subscribe(result  => {
                this.finish.next(this.createUserModel());
                this.resetForm();
                this.router.navigate([this.navigationUrl],{ queryParams: {brandNew: true, email: value.email }});
            }, errors => {
                this.isRequesting = false;
                this.errors = errors.error.Item1.Errors[0].Description;
            });
        }
    }

    createUserModel(){
      var model: User = {
        id: '',
        userName: this.registerForm.controls['username'].value,
        firstName: this.registerForm.controls['firstName'].value,
        lastName: this.registerForm.controls['lastName'].value,
        fullName: '',
        email: this.registerForm.controls['email'].value,
        role: this.registerForm.controls['role'].value
      }

      return model;
    }

    resetForm(){
      this.registerForm.reset();
      this.registerForm.value['role'] = 'User';
    }
  
    get f(): { [key: string]: AbstractControl } {
      return this.registerForm.controls;
    }
}

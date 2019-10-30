import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { AuthenticationService } from 'src/app/services';
import { Router } from '@angular/router';
import { first, timeout } from 'rxjs/operators';
import { Messages } from 'src/app/models';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {

  loginForm: FormGroup;
  loading = false;
  submitted = false;
  error = '';
  log: Messages[] = [];

  constructor(private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private router: Router) {
        // if (this.authenticationService.currentUserValue) { 
        //   this.router.navigate(['/home']);
        // }
   }

  ngOnInit() {
  //   this.loginForm = this.formBuilder.group({
  //     username: ['', Validators.required],
  //     password: ['', Validators.required]
  // });
    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(2)])
    });
  }

  gotoHome(){
    this.router.navigate(['/home']);
  }

  onSubmit() {
    this.submitted = true;



    if (this.loginForm.invalid) {
        return;
    }

    this.loading = true;
    this.authenticationService.login(this.loginForm.controls.username.value, this.loginForm.controls.password.value)
        .pipe(first())
        .subscribe(
            data => {
                this.router.navigate(['/home']);
            },
            error => {
                this.error = error;
                this.loading = false;
            });
}

}

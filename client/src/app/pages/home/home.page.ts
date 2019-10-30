import { Component } from '@angular/core';
import { AuthenticationService, UserService } from 'src/app/services';
import { first } from 'rxjs/operators';
import { User } from 'src/app/models';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  constructor(private authenticationService: AuthenticationService, private userService: UserService) {}
  users: User[];


  ngOnInit() {
    
    this.userService.getAll().pipe(first()).subscribe(users => {
        this.users = users;
    });
}

  logout(){
    this.authenticationService.logout();
  }

}

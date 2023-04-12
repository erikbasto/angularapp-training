import { Component, OnInit } from '@angular/core';
import { AccountService } from './services/account.service';
import { User } from './modules/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Hellou!';

  constructor(private accountService:AccountService){
  }

  ngOnInit(): void {
  }



  setCurrentUser(){
    const userString = localStorage.getItem('user');
    if (!userString)  return;
    const user:User = JSON.parse(userString);
    this.accountService.setCurrentUser(user);
  }

}

import {Component, OnInit} from '@angular/core';
import {OidcSecurityService} from "angular-auth-oidc-client";

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit{
  user: any;

  constructor(private readonly oidcSecurityService: OidcSecurityService) {
  }
  ngOnInit() {
    this.oidcSecurityService.getUserData().subscribe((userData: any) => {
      this.user = userData;
    });
  }
}

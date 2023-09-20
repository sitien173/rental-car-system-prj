import {Component, Inject, OnInit, Renderer2} from '@angular/core';
import {DOCUMENT} from "@angular/common";
import {OidcSecurityService} from "angular-auth-oidc-client";
import {ActivatedRoute} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  isAuth: boolean = false;
  constructor(private readonly _renderer2: Renderer2,
              private readonly _activatedRoute: ActivatedRoute,
              private readonly _oidcSecurityService: OidcSecurityService,
              @Inject(DOCUMENT) private readonly _document: Document) {
  }

  async ngOnInit() {
    let script = this._renderer2.createElement('script');
    script.type = `text/javascript`;
    script.src = `https://cdn.jsdelivr.net/npm/admin-lte@3.2/dist/js/adminlte.min.js`;
    this._renderer2.appendChild(this._document.body, script);

    this._oidcSecurityService
      .checkAuth()
      .subscribe(
        (authenticated) => {
          this.isAuth = authenticated.isAuthenticated;

          if (!this.isAuth) {
            this._oidcSecurityService.authorize();
          }
        }
      );
  }

}

import {Component, Inject, OnInit, Renderer2} from '@angular/core';
import {DOCUMENT} from "@angular/common";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit{
  constructor(private readonly _renderer2: Renderer2,
              @Inject(DOCUMENT) private readonly _document: Document) {
  }

  ngOnInit(): void {
    let script = this._renderer2.createElement('script');
    script.type = `text/javascript`;
    script.src = `assets/js/main.js`;
    this._renderer2.appendChild(this._document.body, script);
  }
}

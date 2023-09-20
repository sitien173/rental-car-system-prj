import {Component, Inject, OnInit, Renderer2} from '@angular/core';
import {DOCUMENT} from "@angular/common";

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit{
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

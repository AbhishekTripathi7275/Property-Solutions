import { Component, Input } from '@angular/core';
import { IPropertyBase } from '../../model/ipropertybase';

@Component({
  selector: 'app-property-card',
  templateUrl: 'property-card.component.html',
  styleUrl: 'property-card.component.css',
})
export class PropertyCardComponent {
  @Input() property: IPropertyBase | undefined;
  @Input() hideIcons!: boolean;
}

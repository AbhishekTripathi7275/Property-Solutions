import { Component, OnInit, ViewChild } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { TabsetComponent } from 'ngx-bootstrap/tabs';
import { IPropertyBase } from '../../model/ipropertybase';
import { Property } from '../../model/property';
import { PropertiesService } from '../../services/properties.service';
import { AlertyfyService } from '../../services/alertyfy.service';
import { Ikeyvaluepair } from '../../model/ikeyvaluepair';
import { DatePipe } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-add-property',
  templateUrl: './add-property.component.html',
  styleUrls: ['./add-property.component.css'],
})
export class AddPropertyComponent implements OnInit {
  @ViewChild('formTabs', { static: false }) formTabs?: TabsetComponent;

  selectedFiles?: FileList;
  previews: string[] = [];

  addPropertyForm!: FormGroup;
  nextClicked!: boolean;
  property = new Property();

  propertyType!: Ikeyvaluepair[];
  furnishType!: Ikeyvaluepair[];
  cityList!: any[];

  propertyView: IPropertyBase = {
    id: null!,
    name: '',
    price: null!,
    sellRent: null!,
    propertyType: null!,
    furnishingType: null!,
    bhk: null!,
    builtArea: null!,
    city: '',
    readyToMove: null!,
  };

  constructor(
    private datePipe: DatePipe,
    private fb: FormBuilder,
    private route: Router,
    private housingService: PropertiesService,
    private alertyfy: AlertyfyService
  ) { }

  ngOnInit() {

    if (!localStorage.getItem('userName')) {
      this.alertyfy.error("You must be logged in to add a property");
      this.route.navigate(['/user/login']);
    }

    this.CreateAddPropertyForm();
    this.housingService.getAllCities().subscribe((data) => {
      this.cityList = data;
      console.log(data);
    });
    this.housingService.getPropertyTypes().subscribe((data) => {
      this.propertyType = data;
      console.log(data);
    });
    this.housingService.getFurnishingTypes().subscribe((data) => {
      this.furnishType = data;
      console.log(data);
    });
  }

  CreateAddPropertyForm() {
    this.addPropertyForm = this.fb.group({
      BasicInfo: this.fb.group({
        SellRent: ['1', Validators.required],
        BHK: [null, Validators.required],
        PType: [null, Validators.required],
        FType: [null, Validators.required],
        Name: [null, Validators.required],
        City: [null, Validators.required],
      }),
      PriceInfo: this.fb.group({
        Price: [null, Validators.required],
        BuiltArea: [null, Validators.required],
        CarpetArea: [null],
        Security: [null],
        Maintenance: [null],
      }),
      AddressInfo: this.fb.group({
        FloorNo: [null],
        TotalFloor: [null],
        Address: [null, Validators.required],
        LandMark: [''],
      }),
      OtherInfo: this.fb.group({
        RTM: [null, Validators.required],
        PossessionOn: [null, Validators.required],
        AOP: [null],
        Gated: [null],
        MainEntrance: [''],
        Description: [''],
      }),
      Photos: this.fb.group({
        ImageFiles: ['', [Validators.required]],
      }),
    });
  }

  //----------------
  // Getter Method
  //----------------

  // #region <FormGroups>
  get BasicInfo() {
    return this.addPropertyForm.controls['BasicInfo'] as FormGroup;
  }

  get PriceInfo() {
    return this.addPropertyForm.controls['PriceInfo'] as FormGroup;
  }

  get AddressInfo() {
    return this.addPropertyForm.controls['AddressInfo'] as FormGroup;
  }

  get OtherInfo() {
    return this.addPropertyForm.controls['OtherInfo'] as FormGroup;
  }
  get Photos() {
    return this.addPropertyForm.controls['Photos'] as FormGroup;
  }
  // #endregion
  //#region <Form Controls>
  get SellRent() {
    return this.BasicInfo.controls['SellRent'] as FormControl;
  }

  get BHK() {
    return this.BasicInfo.controls['BHK'] as FormControl;
  }

  get PType() {
    return this.BasicInfo.controls['PType'] as FormControl;
  }

  get FType() {
    return this.BasicInfo.controls['FType'] as FormControl;
  }

  get Name() {
    return this.BasicInfo.controls['Name'] as FormControl;
  }

  get City() {
    return this.BasicInfo.controls['City'] as FormControl;
  }

  get Price() {
    return this.PriceInfo.controls['Price'] as FormControl;
  }

  get BuiltArea() {
    return this.PriceInfo.controls['BuiltArea'] as FormControl;
  }

  get CarpetArea() {
    return this.PriceInfo.controls['CarpetArea'] as FormControl;
  }

  get Security() {
    return this.PriceInfo.controls['Security'] as FormControl;
  }

  get Maintenance() {
    return this.PriceInfo.controls['Maintenance'] as FormControl;
  }

  get FloorNo() {
    return this.AddressInfo.controls['FloorNo'] as FormControl;
  }

  get TotalFloor() {
    return this.AddressInfo.controls['TotalFloor'] as FormControl;
  }

  get Address() {
    return this.AddressInfo.controls['Address'] as FormControl;
  }

  get LandMark() {
    return this.AddressInfo.controls['LandMark'] as FormControl;
  }

  get RTM() {
    return this.OtherInfo.controls['RTM'] as FormControl;
  }

  get PossessionOn() {
    return this.OtherInfo.controls['PossessionOn'] as FormControl;
  }

  get AOP() {
    return this.OtherInfo.controls['AOP'] as FormControl;
  }

  get Gated() {
    return this.OtherInfo.controls['Gated'] as FormControl;
  }

  get MainEntrance() {
    return this.OtherInfo.controls['MainEntrance'] as FormControl;
  }

  get Description() {
    return this.OtherInfo.controls['Description'] as FormControl;
  }

  get ImageFiles() {
    return this.Photos.controls['ImageFiles'] as FormControl;
  }
  //#endregion


  onBack() {
    this.route.navigate(['/']);
  }
  onSubmit() {
    this.nextClicked = true;
    if (this.allTabsValid()) {
      this.mapProperty();
      this.housingService.addProperty(this.property).subscribe((data) => {
        console.log(data);
        //Image Upload call
        this.ImageUpload(parseInt(JSON.stringify(data)));

        this.alertyfy.success(
          'Congrats, your property listed successfully on our website'
        );
        console.log(this.addPropertyForm);

        if (this.SellRent.value === '2') {
          this.route.navigate(['/rent-property']);
        } else {
          this.route.navigate(['/']);
        }
      });
    } else {
      this.alertyfy.error(
        'Please review the form and provide all valid entries'
      );
      console.log(this.addPropertyForm);
    }
  }

  mapProperty(): void {
    this.property.id = this.housingService.newPropID();
    this.property.sellRent = +this.SellRent.value;
    this.property.bhk = this.BHK.value;
    this.property.propertyTypeId = this.PType.value;
    this.property.name = this.Name.value;
    this.property.cityId = this.City.value;
    this.property.furnishingTypeId = this.FType.value;
    this.property.price = this.Price.value;
    this.property.security = this.Security.value === null ? 0 : this.Security.value;
    this.property.maintenance = this.Maintenance.value === null ? 0 : this.Maintenance.value;
    this.property.builtArea = this.BuiltArea.value;
    this.property.carpetArea = this.CarpetArea.value === null ? 0 : this.CarpetArea.value;
    this.property.floorNo = this.FloorNo.value === null ? 0 : this.FloorNo.value;
    this.property.totalFloors = this.TotalFloor.value === null ? 0 : this.TotalFloor.value;
    this.property.address = this.Address.value;
    this.property.address2 = this.LandMark.value;
    this.property.readyToMove = this.RTM.value === 'true' ? true : false;
    this.property.gated = this.Gated.value === 'true' ? true : false;
    this.property.mainEntrance = this.MainEntrance.value;
    this.property.estPossessionOn = this.datePipe.transform(this.PossessionOn.value, 'yyyy-MM-dd') || '';
    this.property.description = this.Description.value;
  }

  allTabsValid(): boolean {
    if (this.BasicInfo.invalid) {
      if (this.formTabs?.tabs[0]) {
        this.formTabs.tabs[0].active = true;
        return false;
      }
    }
    if (this.PriceInfo.invalid) {
      if (this.formTabs?.tabs[1]) {
        this.formTabs.tabs[1].active = true;
        return false;
      }
    }
    if (this.AddressInfo.invalid) {
      if (this.formTabs?.tabs[2]) {
        this.formTabs.tabs[2].active = true;
        return false;
      }
    }

    if (this.OtherInfo.invalid) {
      if (this.formTabs?.tabs[3]) {
        this.formTabs.tabs[3].active = true;
        return false;
      }
    }
    if (this.Photos.invalid) {
      if (this.formTabs?.tabs[4]) {
        this.formTabs.tabs[4].active = true;
        return false;
      }
    }
    return true;
  }

  selectTab(tabId: number, IsCurrentTabValid: boolean) {
    this.nextClicked = true;
    if (IsCurrentTabValid) {
      if (this.formTabs?.tabs[tabId]) {
        this.formTabs.tabs[tabId].active = true;
      }
    }
  }

  selectFiles(event: any): void {
    this.selectedFiles = event.target.files;

    if (this.selectedFiles && this.selectedFiles[0]) {
      const numberOfFiles = this.selectedFiles.length;
      if (this.previews.length <= 4) {
        for (let i = 0; i < numberOfFiles; i++) {
          const reader = new FileReader();

          reader.onload = (e: any) => {
            this.previews.push(e.target.result);
          };

          reader.readAsDataURL(this.selectedFiles[i]);
        }
      } else {
        this.alertyfy.error('Maximum 5 images allowed');
      }
    }
  }
  removeSelectedFile(index: any) {
    this.previews.splice(index, 1);
  }

  ImageUpload(PropertyId: number) {
    if (this.selectedFiles) {
      this.housingService
        .uploadImage(this.selectedFiles, PropertyId)
        .subscribe((p) => {
          this.alertyfy.success('Image Uploaded');
        });
    } else {
      this.alertyfy.error('Image Not selected');
    }
  }


}

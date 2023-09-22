import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
export * from './additional-fees/additional-fees';
export * from './additional-fees/create-additional-fees';
export * from './additional-fees/update-additional-fees';
export * from './brands/brand';
export * from './brands/create-brand';
export * from './brands/update-brand';
export * from './cars/car';
export * from './cars/car-item';
export * from './cars/create-car';
export * from './cars/update-car';
export * from './cartype/cartype';
export * from './cartype/create-cartype';
export * from './cartype/update-cartype';
export * from './common/icon';
export * from './common/image';
export * from './common/page';
export * from './common/select-list-item';
export * from './common/upload-file-response';
export * from './damage-assessments/damage-assessment';
export * from './damage-assessments/create-damage-assessment';
export * from './damage-assessments/update-damage-assessment';
export * from './features/feature';
export * from './features/create-feature';
export * from './features/update-feature';
export * from './payments/payment';
export * from './payments/create-payment';
export * from './rental-contracts/rental-contract';
export * from './rental-contracts/create-contract';
export * from './rental-contracts/update-contract';
export * from './rental-documents/rental-documents';
export * from './rental-documents/create-rental-documents';
export * from './rental-documents/update-rental-documents';
export * from './rental-request/rental-request';
export * from './vehicle-handover/vehicle-handover';
export * from './vehicle-handover/create-vehicle-handover';
export * from './vehicle-handover/update-vehicle-handover';

@NgModule({
  imports: [CommonModule],
})
export class DataModelsModule {}

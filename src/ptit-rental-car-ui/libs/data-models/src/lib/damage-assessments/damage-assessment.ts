export interface DamageAssessment {
    id: string;
    assessmentDate: Date;
    damageDescription: string;
    repairCost: number;
    totalCost: number;
    rentalContractId: string;
}

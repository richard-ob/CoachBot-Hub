import { Match } from './match.model';
import { TournamentGroup } from './tournament-group.model';

export interface TournamentGroupMatch {
    id: number;
    match: Match;
    tournamentGroupId: number;
    tournamentPhaseId: number;
    teamHomePlaceholder: string;
    teamAwayPlaceholder: string;
    tournamentGroup: TournamentGroup;
}
